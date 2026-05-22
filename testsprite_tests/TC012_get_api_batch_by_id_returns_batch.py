import requests

BASE_URL = "http://localhost:5243"

def test_get_api_batch_get_all_returns_batches():
    # Authenticate to get JWT token
    login_url = f"{BASE_URL}/Auth/Account/Login"
    login_payload = {
        "UserName": "SuperAdmin@gmail.com",
        "Password": "Admin$1234",
        "RememberMe": False
    }
    try:
        login_resp = requests.post(login_url, json=login_payload, timeout=30)
        login_resp.raise_for_status()
        login_data = login_resp.json()
        token = login_data.get("token")
        assert token, "Login succeeded but no token returned"
    except requests.RequestException as e:
        raise AssertionError(f"Login request failed: {e}")
    except (ValueError, KeyError):
        raise AssertionError("Invalid JSON or missing token in login response")

    headers = {
        "Authorization": f"Bearer {token}"
    }

    # Test GET /Api/Batch/GetAll
    get_url = f"{BASE_URL}/Api/Batch/GetAll"
    try:
        response = requests.get(get_url, headers=headers, timeout=30)
    except requests.RequestException as e:
        raise AssertionError(f"GET request failed: {e}")

    assert response.status_code == 200, f"Expected status code 200 but got {response.status_code}"
    try:
        resp_json = response.json()
    except ValueError:
        raise AssertionError("Response is not valid JSON")

    # Expect list of batches
    assert isinstance(resp_json, list), "Response JSON is not a list"
    # If list not empty, check minimal fields
    if resp_json:
        first_batch = resp_json[0]
        assert isinstance(first_batch, dict), "Batch item is not an object"
        assert 'id' in first_batch, "'id' field missing in batch item"
        assert 'courseId' in first_batch or 'CourseId' in first_batch, "'CourseId' field missing in batch item"


test_get_api_batch_get_all_returns_batches()
