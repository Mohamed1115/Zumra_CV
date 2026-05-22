import requests

BASE_URL = "http://localhost:5243"

def test_get_api_facility_success_invalid_session():
    url = f"{BASE_URL}/Api/Facility/Success"
    params = {"session_id": "invalid_session"}
    try:
        response = requests.get(url, params=params, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 400, f"Expected status 400 but got {response.status_code}"
    # Optionally check response json for error indication if any
    try:
        json_data = response.json()
        # Could be {success: false} or error message indicating bad session
        assert "success" in json_data and json_data["success"] is False or "error" in json_data or "message" in json_data, \
            f"Unexpected response body: {json_data}"
    except ValueError:
        # if response is not JSON, still ok, just ensure 400 status
        pass

test_get_api_facility_success_invalid_session()