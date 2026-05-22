import requests


def test_get_api_group_returns_all_groups_for_facility():
    base_url = "http://localhost:5243"
    timeout = 30

    # Test with facilityId=1
    url_1 = f"{base_url}/api/Group/1"
    response_1 = requests.get(url_1, timeout=timeout)
    assert response_1.status_code == 200, f"Expected status 200, got {response_1.status_code}"
    resp_json_1 = response_1.json()
    # We expect keys success and data, success must be True, data must be an array (list)
    assert "success" in resp_json_1, "Response missing 'success' field"
    assert resp_json_1["success"] is True, "Success field is not True"
    assert "data" in resp_json_1, "Response missing 'data' field"
    assert isinstance(resp_json_1["data"], list), "Data field is not a list"

    # Test with facilityId=999999 (non-existent)
    url_2 = f"{base_url}/api/Group/999999"
    response_2 = requests.get(url_2, timeout=timeout)
    assert response_2.status_code == 200, f"Expected status 200, got {response_2.status_code}"
    resp_json_2 = response_2.json()
    assert "success" in resp_json_2, "Response missing 'success' field"
    # success might be true or false, as no auth and no 404 specified, but test expects success true
    assert resp_json_2["success"] is True, "Success field is not True for facilityId=999999"
    assert "data" in resp_json_2, "Response missing 'data' field for facilityId=999999"
    assert isinstance(resp_json_2["data"], list), "Data field is not a list for facilityId=999999"
    # Data should be empty for facilityId=999999
    assert len(resp_json_2["data"]) == 0, "Data field is not empty for non-existent facilityId=999999"


test_get_api_group_returns_all_groups_for_facility()