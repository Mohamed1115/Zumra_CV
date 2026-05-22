import requests

BASE_URL = "http://localhost:5243"
LOGIN_URL = f"{BASE_URL}/Auth/Account/Login"
DELETE_TASK_URL = f"{BASE_URL}/Api/Task/Delete/999999/1"

USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"

def get_jwt_token():
    try:
        resp = requests.post(
            LOGIN_URL,
            json={"UserName": USERNAME, "Password": PASSWORD, "RememberMe": False},
            timeout=30
        )
        resp.raise_for_status()
        data = resp.json()
        assert data.get("success") is True, "Login success is False"
        token = data.get("token")
        assert token and isinstance(token, str), "Token missing or invalid"
        return token
    except Exception as ex:
        raise RuntimeError(f"Authentication failed: {ex}")

def test_delete_task_nonexistent_facility_returns_404():
    token = get_jwt_token()
    headers = {"Authorization": f"Bearer {token}"}
    try:
        response = requests.delete(DELETE_TASK_URL, headers=headers, timeout=30)
    except Exception as e:
        raise RuntimeError(f"Request failed: {e}")

    assert response.status_code == 404, f"Expected status 404 but got {response.status_code}"
    try:
        json_resp = response.json()
    except Exception:
        raise AssertionError("Response is not valid JSON")

    assert json_resp.get("success") is False, "success field is not False"
    assert (
        json_resp.get("message") == "Facility not found"
    ), f"Unexpected message: {json_resp.get('message')}"

test_delete_task_nonexistent_facility_returns_404()