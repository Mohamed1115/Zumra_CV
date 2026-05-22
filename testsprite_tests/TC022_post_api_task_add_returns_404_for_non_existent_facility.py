import requests

BASE_URL = "http://localhost:5243"
LOGIN_URL = f"{BASE_URL}/Auth/Account/Login"
TASK_CREATE_URL = f"{BASE_URL}/Api/Task/Create"
TIMEOUT = 30

USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"

def test_post_api_task_create_with_invalid_course_and_lesson_ids():
    # Login to get JWT token
    login_payload = {
        "UserName": USERNAME,
        "Password": PASSWORD,
        "RememberMe": False
    }
    try:
        login_response = requests.post(LOGIN_URL, json=login_payload, timeout=TIMEOUT)
        login_response.raise_for_status()
    except requests.RequestException as e:
        assert False, f"Login request failed: {e}"

    login_json = login_response.json()
    assert login_json.get("success") is True, "Login failed, success not true"
    token = login_json.get("token")
    assert token, "Token not found in login response"

    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json"
    }

    # Use non-existent CourseId and LessonId which should cause validation error (likely 400)
    payload = {
        "CourseId": 999999,
        "LessonId": 999999,
        "Title": "Test Task",
        "Description": "Test task description",
        "DueDate": "2026-12-31T23:59:59Z"
    }

    try:
        response = requests.post(TASK_CREATE_URL, json=payload, headers=headers, timeout=TIMEOUT)
    except requests.RequestException as e:
        assert False, f"POST request to create task failed: {e}"

    # Expecting 400 Bad Request due to invalid CourseId or LessonId
    assert response.status_code == 400, f"Expected status 400, got {response.status_code}"

    try:
        resp_json = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    # Verify success is false in response
    success_val = resp_json.get("success")
    assert success_val is False, "Expected success=false in response"

    # Optionally check for error message or errors field
    message = resp_json.get("message", "")
    errors = resp_json.get("errors", [])
    assert ("not found" in message.lower() or errors), f"Expected error message or errors in response, got '{message}' and errors: {errors}"


test_post_api_task_create_with_invalid_course_and_lesson_ids()
