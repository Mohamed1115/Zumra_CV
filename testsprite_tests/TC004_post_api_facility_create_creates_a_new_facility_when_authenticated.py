import requests

BASE_URL = "http://localhost:5243"
LOGIN_ENDPOINT = "/Auth/Account/Login"
FACILITY_CREATE_ENDPOINT = "/Api/Facility/Create"
FACILITY_DELETE_ENDPOINT = "/Api/Facility/Delete"


def test_post_api_facility_create_creates_new_facility_when_authenticated():
    login_url = BASE_URL + LOGIN_ENDPOINT
    create_url = BASE_URL + FACILITY_CREATE_ENDPOINT
    delete_url = BASE_URL + FACILITY_DELETE_ENDPOINT

    # Login payload with provided credentials
    login_payload = {
        "UserName": "SuperAdmin@gmail.com",
        "Password": "Admin$1234",
        "RememberMe": False
    }

    try:
        # Authenticate and get JWT token
        login_resp = requests.post(login_url, json=login_payload, timeout=30)
        assert login_resp.status_code == 200, f"Login failed with status code {login_resp.status_code}"
        login_json = login_resp.json()
        assert login_json.get("success") is True, f"Login response indicates failure: {login_json}"
        token = login_json.get("token")
        assert isinstance(token, str) and token, "JWT token missing or invalid in login response"

        headers = {
            "Authorization": f"Bearer {token}"
        }

        # Facility create form-data; use files with (None, value) tuples to force multipart/form-data encoding
        facility_data = {
            "Name": (None, "Test Facility Automation"),
            "Description": (None, "Created by automated test"),
            "Type": (None, "Academy"),
            "CategoryId": (None, "1")
        }

        # POST request to create facility with form-data
        create_resp = requests.post(create_url, headers=headers, files=facility_data, timeout=30)

        assert create_resp.status_code == 201, f"Expected 201 Created, got {create_resp.status_code}"
        create_json = create_resp.json()
        assert create_json.get("success") is True, f"Facility create response indicates failure: {create_json}"
        data = create_json.get("data")
        assert data and isinstance(data, dict), "Facility create response missing data object"
        facility_id = data.get("id")
        assert facility_id is not None, "Facility ID missing in create response"

    finally:
        # Cleanup: attempt to delete the created facility if facility_id exists
        if 'facility_id' in locals() and facility_id is not None:
            try:
                delete_resp = requests.delete(delete_url, headers=headers, params={"id": facility_id}, timeout=30)
                if delete_resp.status_code not in (200, 403):
                    print(f"Unexpected status code when deleting facility: {delete_resp.status_code}, response: {delete_resp.text}")
            except Exception as e:
                print(f"Exception during cleanup delete: {e}")


test_post_api_facility_create_creates_new_facility_when_authenticated()