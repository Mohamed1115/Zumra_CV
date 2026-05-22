import requests
from requests.auth import HTTPBasicAuth

def test_get_auth_account_externallogin_google_redirect():
    base_url = "http://localhost:5243"
    endpoint = "/Auth/Account/ExternalLogin"
    params = {
        "provider": "google",
        "returnUrl": "/"
    }
    auth = HTTPBasicAuth("SuperAdmin@gmail.com", "Admin$1234")
    try:
        response = requests.get(
            f"{base_url}{endpoint}",
            params=params,
            auth=auth,
            allow_redirects=False,
            timeout=30
        )
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 302, f"Expected status code 302, got {response.status_code}"

    location = response.headers.get("Location", "")
    assert location.lower().startswith("https://accounts.google.com") or "google" in location.lower(), \
        f"Expected redirect location to Google OAuth, got: {location}"

test_get_auth_account_externallogin_google_redirect()