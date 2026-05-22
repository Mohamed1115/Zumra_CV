import requests

def test_get_api_course_returns_all_courses():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Course/GetAll"
    headers = {
        "Accept": "application/json"
    }
    try:
        response = requests.get(url, headers=headers, timeout=30)
        assert response.status_code == 200, f"Expected status code 200, got {response.status_code}"
        json_data = response.json()
        # The PRD states the response schema to be Course[] on success (an array).
        # The test description expects success=true and data array, so test accordingly.
        # However, the PRD sample shows only array data, no wrapper.
        # We will handle both cases: if 'success' key exists or not.
        if isinstance(json_data, dict):
            # Possibly wrapped response
            assert "success" in json_data, "Response JSON missing 'success' key"
            assert json_data["success"] is True, "Response 'success' is not True"
            assert "data" in json_data, "Response JSON missing 'data' key"
            assert isinstance(json_data["data"], list), "'data' is not a list"
        elif isinstance(json_data, list):
            # Direct array of courses
            # Since no 'success' key, just accept empty list or list of dicts
            assert all(isinstance(course, dict) for course in json_data), "Not all items in response array are objects"
        else:
            assert False, "Unexpected response format"
    except requests.exceptions.RequestException as e:
        assert False, f"Request to {url} failed with exception: {e}"

test_get_api_course_returns_all_courses()