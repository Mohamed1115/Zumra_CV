import requests

base_url = "http://localhost:5243"
timeout = 30

def test_post_auth_account_resetpassword_resets_password_after_otp_verification():
    # Use a test user email that has completed OTP verification step
    # Since the OTP verification is required before password reset, 
    # we will simulate the entire flow including ForgotPassword and VerifyOTP

    test_email = "testuser_resetpassword@example.com"
    new_password = "NewPassw0rd$123"

    # Step 1: Send ForgotPassword request to get OTP sent (we assume OTP is obtainable for test)
    forgot_password_url = f"{base_url}/Auth/Account/ForgotPassword"
    forgot_payload = {"Email": test_email}
    resp_forgot = requests.post(forgot_password_url, json=forgot_payload, timeout=timeout)
    assert resp_forgot.status_code == 200
    json_forgot = resp_forgot.json()
    assert json_forgot.get("success") is True

    # For testing, since we do not have real OTP, simulate OTP retrieval or use a fixed test OTP
    # In real test environment, this OTP should be retrieved from test doubles, mocks or email test inbox
    test_otp = "123456"  # Placeholder OTP for testing

    # Step 2: Verify OTP
    verify_otp_url = f"{base_url}/Auth/Account/VerifyOTP"
    verify_payload = {"Email": test_email, "Otp": test_otp}
    resp_verify = requests.post(verify_otp_url, json=verify_payload, timeout=timeout)
    # If OTP is incorrect, expect 400; for test assume success
    assert resp_verify.status_code == 200
    json_verify = resp_verify.json()
    assert json_verify.get("success") is True

    # Step 3: Reset Password
    reset_password_url = f"{base_url}/Auth/Account/ResetPassword"
    reset_payload = {"Email": test_email, "Password": new_password}
    resp_reset = requests.post(reset_password_url, json=reset_payload, timeout=timeout)
    assert resp_reset.status_code == 200
    json_reset = resp_reset.json()
    assert json_reset.get("success") is True

test_post_auth_account_resetpassword_resets_password_after_otp_verification()
