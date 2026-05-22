import requests

BASE_URL = "http://localhost:5243"
FACILITY_GETBYID_URL = f"{BASE_URL}/Api/Facility/GetById"

def test_get_api_facility_getbyid_returns_single_facility():
    timeout = 30
    # This endpoint does not require authentication according to PRD, so no token needed
    # Test with existing facility id = 1
    existing_id = 1
    resp1 = requests.get(f"{FACILITY_GETBYID_URL}/{existing_id}", timeout=timeout)
    assert resp1.status_code == 200, f"Expected 200 but got {resp1.status_code}"
    json1 = resp1.json()
    assert json1.get("success") is True, "Expected success=True for existing facility id"
    data1 = json1.get("data")
    assert isinstance(data1, dict), "Expected data to be an object for existing facility"
    assert "id" in data1 and data1["id"] == existing_id, "Facility ID in data does not match requested ID"

    # Test with invalid facility id = 999999
    invalid_id = 999999
    resp2 = requests.get(f"{FACILITY_GETBYID_URL}/{invalid_id}", timeout=timeout)
    assert resp2.status_code == 404, f"Expected 404 but got {resp2.status_code}"
    json2 = resp2.json()
    assert json2.get("success") is False, "Expected success=False for non-existent facility id"

test_get_api_facility_getbyid_returns_single_facility()