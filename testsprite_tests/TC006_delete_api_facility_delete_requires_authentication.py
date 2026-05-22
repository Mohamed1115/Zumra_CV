import requests

BASE_URL = "http://localhost:5243"
DELETE_FACILITY_URL = f"{BASE_URL}/Api/Facility/Delete"
TIMEOUT = 30

def test_delete_api_facility_delete_requires_authentication():
    try:
        response = requests.delete(DELETE_FACILITY_URL, timeout=TIMEOUT)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 401, f"Expected status code 401, got {response.status_code}"
    # The response content may vary, optionally check presence of unauthorized message
    # Example: assert "Unauthorized" in response.text or in JSON message if available

test_delete_api_facility_delete_requires_authentication()