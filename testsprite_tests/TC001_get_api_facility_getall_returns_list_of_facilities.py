import requests

def test_get_api_facility_getall_returns_list_of_facilities():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Facility/GetAll"
    try:
        response = requests.get(url, timeout=30)
        assert response.status_code == 200, f"Expected status 200 but got {response.status_code}"
        json_response = response.json()
        assert "success" in json_response, "Response JSON missing 'success' key"
        assert json_response["success"] is True, "Response 'success' is not True"
        assert "data" in json_response, "Response JSON missing 'data' key"
        assert isinstance(json_response["data"], list), "Response 'data' is not a list"
    except requests.RequestException as e:
        assert False, f"HTTP request failed: {e}"

test_get_api_facility_getall_returns_list_of_facilities()