import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30

def test_post_api_section_create_returns_404_for_non_existent_facility():
    url = f"{BASE_URL}/Api/Section/999999/1"
    payload = {
        "Name": "Test Section",
        "Order": 1,
        "CourseId": 1,
        "CourseBatchId": 1
    }
    headers = {
        "Content-Type": "application/json"
    }

    try:
        response = requests.post(url, json=payload, headers=headers, timeout=TIMEOUT)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 404, f"Expected status code 404 but got {response.status_code}"
    try:
        body = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    assert body.get("success") is False, f"Expected success=false but got {body.get('success')}"
    assert "Facility not found" in body.get("message", ""), f"Expected message to contain 'Facility not found' but got {body.get('message')}"

test_post_api_section_create_returns_404_for_non_existent_facility()