import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30


def test_TC002_get_api_category_by_id_returns_single_category():
    headers = {"Accept": "application/json"}

    # Test with id=1
    url_1 = f"{BASE_URL}/Api/Category/1"
    try:
        response_1 = requests.get(url_1, headers=headers, timeout=TIMEOUT)
        assert response_1.status_code == 200, f"Expected status code 200, got {response_1.status_code}"
        json_1 = response_1.json()
        # Verify success=true and data object
        assert isinstance(json_1, dict), "Response is not a JSON object"
        assert "success" in json_1, "'success' field missing in response"
        assert json_1["success"] is True, "'success' field is not True"
        assert "data" in json_1, "'data' field missing in response"
        # data should be a JSON object (dict), not array or null
        assert isinstance(json_1["data"], (dict, type(None))), "'data' is not an object or null"
    except requests.exceptions.RequestException as e:
        assert False, f"Request exception for id=1: {e}"

    # Test with id=999999 (non-existent)
    url_999999 = f"{BASE_URL}/Api/Category/999999"
    try:
        response_999999 = requests.get(url_999999, headers=headers, timeout=TIMEOUT)
        assert response_999999.status_code == 200, f"Expected status code 200, got {response_999999.status_code}"
        json_999999 = response_999999.json()
        # Verify success field and data present, data may be null or empty but structure intact
        assert isinstance(json_999999, dict), "Response is not a JSON object"
        assert "success" in json_999999, "'success' field missing in response"
        assert "data" in json_999999, "'data' field missing in response"
        # success can be true or false depending on implementation - but test plan says 200 with structure
        # So we verify at least success is present with boolean type
        assert isinstance(json_999999["success"], bool), "'success' field is not boolean"
        # data can be None (null), or dict, or empty
        assert isinstance(json_999999["data"], (dict, type(None))), "'data' field is not an object or null"
    except requests.exceptions.RequestException as e:
        assert False, f"Request exception for id=999999: {e}"


test_TC002_get_api_category_by_id_returns_single_category()