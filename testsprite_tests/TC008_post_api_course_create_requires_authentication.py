import requests

def test_post_api_course_create_requires_authentication():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Course/1"
    headers = {
        "Content-Type": "application/json"
    }
    payload = {
        "Title": "Unauthorized Course Test",
        "Description": "Test course creation without auth",
        "CategoryId": 1,
        "FacilityId": 1,
        "Price": 0
    }

    try:
        response = requests.post(url, json=payload, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 401, f"Expected status code 401, got {response.status_code}"

test_post_api_course_create_requires_authentication()