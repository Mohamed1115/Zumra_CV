import requests
from requests.auth import HTTPBasicAuth

def test_post_auth_account_forgotpassword_sends_otp_code():
    base_url = "http://localhost:5243"
    endpoint = "/Auth/Account/ForgotPassword"
    url = base_url + endpoint
    auth_username = "SuperAdmin@gmail.com"
    auth_password = "Admin$1234"
    
    # Registered email to test OTP sending
    registered_email = "SuperAdmin@gmail.com"
    
    headers = {
        "Content-Type": "application/json"
    }
    payload = {
        "Email": registered_email
    }
    
    try:
        response = requests.post(
            url,
            json=payload,
            headers=headers,
            auth=HTTPBasicAuth(auth_username, auth_password),
            timeout=30
        )
        response.raise_for_status()
        data = response.json()

        # Validate HTTP status code 200
        assert response.status_code == 200, f"Expected 200 OK but got {response.status_code}"
        # Validate response success true
        assert "success" in data, "Response missing 'success' field"
        assert data["success"] == True, f"Expected success True but got {data['success']}"
        # Validate message field exists and is non-empty
        assert "message" in data, "Response missing 'message' field"
        assert isinstance(data["message"], str) and data["message"], "Response message empty or invalid"

    except requests.exceptions.RequestException as e:
        assert False, f"Request failed: {e}"
    except ValueError:
        assert False, "Response is not a valid JSON"

test_post_auth_account_forgotpassword_sends_otp_code()