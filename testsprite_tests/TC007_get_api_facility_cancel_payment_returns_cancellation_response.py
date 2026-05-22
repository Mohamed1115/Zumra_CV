import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30

def test_get_api_facility_cancel_payment_returns_cancellation_response():
    url = f"{BASE_URL}/Api/Facility/Cancel"
    try:
        response = requests.get(url, timeout=TIMEOUT)
        response.raise_for_status()
    except requests.RequestException as e:
        assert False, f"Request to {url} failed: {e}"

    assert response.status_code == 200, f"Expected status code 200, got {response.status_code}"
    try:
        json_resp = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    assert 'success' in json_resp, "Response JSON missing 'success' key"
    assert json_resp['success'] is False, f"Expected success to be False, got {json_resp['success']}"
    # message indicating payment was cancelled might be in message or similar key
    assert any("cancel" in str(json_resp.get(k, "")).lower() for k in json_resp), "Response does not indicate payment was cancelled"

test_get_api_facility_cancel_payment_returns_cancellation_response()