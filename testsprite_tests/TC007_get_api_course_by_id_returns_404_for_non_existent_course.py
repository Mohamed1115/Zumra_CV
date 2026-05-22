import requests

BASE_URL = "http://localhost:5243"
LOGIN_URL = f"{BASE_URL}/Auth/Account/Login"
COURSE_URL = f"{BASE_URL}/Api/Course/999999"
USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"
TIMEOUT = 30

def test_get_api_course_by_id_returns_404_for_nonexistent_course():
    # Login to get JWT token
    login_payload = {
        "UserName": USERNAME,
        "Password": PASSWORD,
        "RememberMe": False
    }
    try:
        login_resp = requests.post(LOGIN_URL, json=login_payload, timeout=TIMEOUT)
        login_resp.raise_for_status()
        login_data = login_resp.json()
        assert login_data.get("success") == True, "Login failed, success not true"
        token = login_data.get("token")
        assert token, "No token received from login"
    except Exception as e:
        raise AssertionError(f"Login failed: {e}")

    headers = {
        "Authorization": f"Bearer {token}"
    }

    # GET non-existent course
    try:
        resp = requests.get(COURSE_URL, headers=headers, timeout=TIMEOUT)
    except Exception as e:
        raise AssertionError(f"GET request failed: {e}")

    # Validate response status code 404 and content
    assert resp.status_code == 404, f"Expected status code 404, got {resp.status_code}"
    try:
        resp_json = resp.json()
    except Exception as e:
        raise AssertionError(f"Response is not valid JSON: {e}")

    assert resp_json.get("success") is False, f"Expected success=false, got {resp_json.get('success')}"
    message = resp_json.get("message")
    assert isinstance(message, str), f"Expected message string in response, got {message}"
    assert message.lower() == "course not found", f"Expected message 'Course not found', got '{message}'"

test_get_api_course_by_id_returns_404_for_nonexistent_course()