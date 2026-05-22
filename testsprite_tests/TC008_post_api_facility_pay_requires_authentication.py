import requests

BASE_URL = "http://localhost:5243"

def test_post_api_facility_pay_requires_authentication():
    url = f"{BASE_URL}/Api/Facility/Pay"
    try:
        response = requests.post(url, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"
    assert response.status_code == 401, f"Expected 401 Unauthorized, got {response.status_code}"

test_post_api_facility_pay_requires_authentication()