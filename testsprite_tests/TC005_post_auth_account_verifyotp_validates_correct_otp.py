import requests
from requests.auth import HTTPBasicAuth

BASE_URL = "http://localhost:5243"
AUTH_USERNAME = "SuperAdmin@gmail.com"
AUTH_PASSWORD = "Admin$1234"
TIMEOUT = 30

def test_post_auth_account_verifyotp_validates_correct_otp():
    session = requests.Session()
    session.auth = HTTPBasicAuth(AUTH_USERNAME, AUTH_PASSWORD)
    try:
        email = "SuperAdmin@gmail.com"

        # Step 1: Trigger ForgotPassword to send OTP
        forgot_password_url = f"{BASE_URL}/Auth/Account/ForgotPassword"
        forgot_payload = {"Email": email}
        forgot_response = session.post(forgot_password_url, json=forgot_payload, timeout=TIMEOUT)
        assert forgot_response.status_code == 200, f"ForgotPassword request failed: {forgot_response.text}"
        forgot_resp_json = forgot_response.json()
        assert forgot_resp_json.get("success") is True, f"ForgotPassword response not successful: {forgot_response.text}"

        # Normally OTP is sent to email.
        # Since this is a test environment, to test VerifyOTP with correct OTP,
        # we need a valid OTP.
        # Because no direct endpoint to get OTP is provided, we assume a test OTP "123456" for demonstration.
        # In real tests, OTP would be retrieved from email system or mocked.

        otp = "123456"  # Placeholder for correct OTP in test environment

        verify_otp_url = f"{BASE_URL}/Auth/Account/VerifyOTP"
        verify_payload = {
            "Email": email,
            "Otp": otp
        }
        verify_response = session.post(verify_otp_url, json=verify_payload, timeout=TIMEOUT)
        assert verify_response.status_code == 200, f"VerifyOTP failed with status {verify_response.status_code}: {verify_response.text}"
        verify_resp_json = verify_response.json()
        assert verify_resp_json.get("success") is True, f"VerifyOTP response not successful: {verify_response.text}"

    finally:
        session.close()

test_post_auth_account_verifyotp_validates_correct_otp()