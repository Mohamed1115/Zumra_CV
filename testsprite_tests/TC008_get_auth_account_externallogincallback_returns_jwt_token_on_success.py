import requests
from requests.auth import HTTPBasicAuth

def test_get_auth_account_externallogincallback_returns_jwt_token_on_success():
    base_url = "http://localhost:5243"
    login_url = f"{base_url}/Auth/Account/Login"
    externallogincallback_url = f"{base_url}/Auth/Account/ExternalLoginCallback"

    auth_username = "SuperAdmin@gmail.com"
    auth_password = "Admin$1234"

    try:
        # Step 1: Login to get a valid session or token if needed to simulate a successful Google OAuth login
        # Since ExternalLoginCallback does not require auth, but test instruction given basic token, 
        # we perform login to validate credentials as a pre-step.
        login_payload = {
            "UserName": auth_username,
            "Password": auth_password,
            "RememberMe": False
        }
        login_resp = requests.post(login_url, json=login_payload, timeout=30)
        assert login_resp.status_code == 200, f"Login failed with status {login_resp.status_code}"
        login_json = login_resp.json()
        assert login_json.get("success") is True, "Login response success flag false"
        token = login_json.get("token")
        assert isinstance(token, str) and token, "JWT token missing in login response"

        # Step 2: Call GET /Auth/Account/ExternalLoginCallback to simulate external login callback
        # No auth required here per PRD
        response = requests.get(externallogincallback_url, timeout=30)
        assert response.status_code == 200, f"Expected HTTP 200 but got {response.status_code}"

        data = response.json()
        # Validate expected fields and values
        assert data.get("success") is True, "Response success flag is not true"
        jwt_token = data.get("token")
        email = data.get("email")
        username = data.get("username")
        assert isinstance(jwt_token, str) and jwt_token, "JWT token missing or empty in response"
        assert isinstance(email, str) and email, "Email missing or empty in response"
        assert isinstance(username, str) and username, "Username missing or empty in response"

    except requests.exceptions.RequestException as e:
        assert False, f"Request failed: {e}"

        
test_get_auth_account_externallogincallback_returns_jwt_token_on_success()