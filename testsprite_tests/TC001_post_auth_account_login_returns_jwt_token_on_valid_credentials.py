import requests

BASE_URL = "http://localhost:5243"
LOGIN_ENDPOINT = "/Auth/Account/Login"
TIMEOUT = 30

def test_post_auth_account_login_returns_jwt_token_on_valid_credentials():
    url = BASE_URL + LOGIN_ENDPOINT
    payload = {
        "UserName": "SuperAdmin@gmail.com",
        "Password": "Admin$1234",
        "RememberMe": False
    }
    headers = {
        "Content-Type": "application/json"
    }
    try:
        response = requests.post(url, json=payload, headers=headers, timeout=TIMEOUT)
    except requests.RequestException as e:
        assert False, f"HTTP request failed: {e}"

    assert response.status_code == 200, f"Expected status 200, got {response.status_code}"
    try:
        json_resp = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"
    
    assert json_resp.get("success") is True, f"Expected success true, got {json_resp.get('success')}"
    token = json_resp.get("token")
    assert token and isinstance(token, str) and len(token) > 0, "JWT token missing or invalid in response"

test_post_auth_account_login_returns_jwt_token_on_valid_credentials()