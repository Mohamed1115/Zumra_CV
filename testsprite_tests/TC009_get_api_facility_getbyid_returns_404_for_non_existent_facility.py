import requests

BASE_URL = "http://localhost:5243"
LOGIN_URL = f"{BASE_URL}/Auth/Account/Login"
FACILITY_GETBYID_URL = f"{BASE_URL}/Api/Facility/GetById/999999"
TIMEOUT = 30
USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"


def test_get_api_facility_getbyid_returns_404_for_non_existent_facility():
    try:
        # Authenticate to obtain JWT token (even though endpoint is public, following instructions)
        login_payload = {
            "UserName": USERNAME,
            "Password": PASSWORD,
            "RememberMe": False
        }
        login_resp = requests.post(LOGIN_URL, json=login_payload, timeout=TIMEOUT)
        login_resp.raise_for_status()
        login_json = login_resp.json()
        assert login_json.get("success") is True, f"Login failed: {login_json}"
        token = login_json.get("token")
        assert token, "No token found in login response"

        headers = {
            "Authorization": f"Bearer {token}"
        }

        # Call the GET facility by id with a non-existent id
        resp = requests.get(FACILITY_GETBYID_URL, headers=headers, timeout=TIMEOUT)

        assert resp.status_code == 404, f"Expected status code 404 but got {resp.status_code}"
        json_resp = resp.json()
        assert "success" in json_resp and json_resp["success"] is False, "Expected success=false in response"
        assert "message" in json_resp, "Response missing 'message' field"
        assert "Facility Not Found" in json_resp["message"], f"Expected 'Facility Not Found' message, got: {json_resp['message']}"

    except requests.RequestException as e:
        assert False, f"Request failed: {e}"


test_get_api_facility_getbyid_returns_404_for_non_existent_facility()