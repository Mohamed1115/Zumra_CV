import requests

BASE_URL = "http://localhost:5243"
LOGIN_URL = f"{BASE_URL}/Auth/Account/Login"
DELETE_LESSON_URL = f"{BASE_URL}/Api/Lesson/Delete/999999/1"
USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"
TIMEOUT = 30

def test_delete_lesson_nonexistent_facility_returns_404():
    # Authenticate and get JWT token
    login_payload = {
        "UserName": USERNAME,
        "Password": PASSWORD,
        "RememberMe": False
    }
    try:
        login_resp = requests.post(LOGIN_URL, json=login_payload, timeout=TIMEOUT)
        login_resp.raise_for_status()
    except requests.RequestException as e:
        raise AssertionError(f"Login request failed: {e}")
    login_data = login_resp.json()
    assert login_data.get("success") is True, f"Login failed: {login_data}"
    token = login_data.get("token")
    assert token and isinstance(token, str), "Token missing or invalid in login response"

    headers = {
        "Authorization": f"Bearer {token}"
    }

    # Send DELETE request to non-existent facility lesson delete endpoint
    try:
        resp = requests.delete(DELETE_LESSON_URL, headers=headers, timeout=TIMEOUT)
    except requests.RequestException as e:
        raise AssertionError(f"DELETE request failed: {e}")

    # Assert status code 404
    assert resp.status_code == 404, f"Expected status code 404, got {resp.status_code}"
    
    # Assert response JSON has success=false and message='Facility not found'
    try:
        data = resp.json()
    except ValueError:
        raise AssertionError("Response is not JSON")

    assert data.get("success") is False, f"Expected success false, got {data.get('success')}"
    message = data.get("message", "")
    assert isinstance(message, str), "Response message is not a string"
    assert message.lower() == "facility not found", f"Expected message 'Facility not found', got '{message}'"

test_delete_lesson_nonexistent_facility_returns_404()