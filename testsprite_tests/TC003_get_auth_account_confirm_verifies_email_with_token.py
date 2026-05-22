import requests
from requests.auth import HTTPBasicAuth

BASE_URL = "http://localhost:5243"
USERNAME = "SuperAdmin@gmail.com"
PASSWORD = "Admin$1234"
TIMEOUT = 30

def test_get_auth_account_confirm_verifies_email_with_token():
    session = requests.Session()
    session.auth = HTTPBasicAuth(USERNAME, PASSWORD)

    # Step 1: Register a new user
    register_url = f"{BASE_URL}/Auth/Account/Register"
    import uuid
    import string
    import random

    # Generate unique email to avoid collision
    random_suffix = ''.join(random.choices(string.ascii_letters + string.digits, k=8))
    test_email = f"autotest_{random_suffix}@example.com"
    test_password = "TestPass123!"
    test_name = "Auto Test User"

    register_payload = {
        "Email": test_email,
        "Password": test_password,
        "Name": test_name
    }

    try:
        reg_resp = session.post(register_url, json=register_payload, timeout=TIMEOUT)
        # Registration should succeed with 200 and success true
        assert reg_resp.status_code == 200, f"Registration failed: {reg_resp.status_code} {reg_resp.text}"
        reg_json = reg_resp.json()
        assert reg_json.get("success") is True, f"Registration not successful: {reg_json}"

        # The rest of the test flow cannot be completed due to lack of API to get token/id for confirm
        raise NotImplementedError("Cannot fetch valid email confirmation token and user id programmatically for testing confirm endpoint.")

    except Exception as e:
        raise e

# Call the test function
test_get_auth_account_confirm_verifies_email_with_token()
