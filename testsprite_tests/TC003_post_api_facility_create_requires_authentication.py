import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30

def test_post_api_facility_create_requires_authentication():
    url = f"{BASE_URL}/Api/Facility/Create"
    # Form-data payload as the API expects form-data, but we test without auth header
    data = {
        "Name": "Test Facility",
        "Description": "A test facility description",
        "Type": "Academy",
        "CategoryId": "1"
    }
    try:
        response = requests.post(url, data=data, timeout=TIMEOUT)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    # Assert the response status is 401 Unauthorized
    assert response.status_code == 401, f"Expected status 401 Unauthorized, got {response.status_code}"

test_post_api_facility_create_requires_authentication()