import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30


def test_put_api_facility_update_requires_authentication():
    # First, get list of facilities to find one valid id for update, no auth required
    get_all_url = f"{BASE_URL}/Api/Facility/GetAll"
    try:
        resp = requests.get(get_all_url, timeout=TIMEOUT)
        resp.raise_for_status()
        data = resp.json()
        assert resp.status_code == 200
        assert data.get("success") is True
        facilities = data.get("data", [])
        if not facilities:
            raise AssertionError("No facilities found to test update endpoint.")
        facility_id = facilities[0].get("id")
        assert facility_id is not None
    except Exception as e:
        raise AssertionError(f"Failed to get facility ID for update test: {e}")

    update_url = f"{BASE_URL}/Api/Facility/Update/{facility_id}"
    # Prepare update payload with all required fields as JSON
    update_payload = {
        "Name": "Test Update Name",
        "Description": "Test Description",
        "Type": "Test Type",
        "CategoryId": 1
    }

    # Make PUT request WITHOUT Authorization header
    response = requests.put(update_url, json=update_payload, timeout=TIMEOUT)

    # Validate that the response status is 401 Unauthorized
    assert response.status_code == 401, f"Expected 401 Unauthorized but got {response.status_code}"


test_put_api_facility_update_requires_authentication()
