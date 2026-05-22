import requests
import uuid

BASE_URL = "http://localhost:5243"
TIMEOUT = 30

def test_post_auth_account_register_sends_confirmation_email():
    url = f"{BASE_URL}/Auth/Account/Register"
    # Create unique user details using uuid
    unique_email = f"testuser_{uuid.uuid4().hex[:8]}@example.com"
    payload = {
        "Email": unique_email,
        "Password": "StrongPassw0rd!",
        "Name": "Test User"
    }
    headers = {
        "Content-Type": "application/json"
    }
    try:
        response = requests.post(url, json=payload, headers=headers, timeout=TIMEOUT)
        # Assert status code is 200
        assert response.status_code == 200, f"Expected status code 200 but got {response.status_code}"
        # Assert response success is true and message contains confirmation info
        json_resp = response.json()
        assert json_resp.get("success") is True, f"Expected success True but got {json_resp.get('success')}"
        assert "message" in json_resp and isinstance(json_resp["message"], str) and len(json_resp["message"]) > 0, \
            "Expected a non-empty message indicating confirmation email sent"
    except (requests.RequestException, AssertionError) as e:
        raise AssertionError(f"Test failed: {e}")

test_post_auth_account_register_sends_confirmation_email()