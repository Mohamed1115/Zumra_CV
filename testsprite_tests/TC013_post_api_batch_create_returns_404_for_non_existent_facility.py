import requests

def test_post_api_batch_create_returns_404_for_non_existent_facility():
    base_url = "http://localhost:5243"
    facility_id = 999999
    url = f"{base_url}/Api/Batch/{facility_id}"
    payload = {
        "CourseId": 1,
        "Title": "Test Batch",
        "StartDate": "2026-01-01",
        "EndDate": "2026-12-31",
        "Capacity": 30,
        "Status": "Active"
    }
    headers = {
        "Content-Type": "application/json"
    }

    try:
        response = requests.post(url, json=payload, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 404, f"Expected status code 404, got {response.status_code}"
    try:
        json_resp = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    # Assert response has success=false and message 'Facility not found'
    assert "success" in json_resp, "Response JSON missing 'success' field"
    assert json_resp["success"] is False, f"Expected success to be false but got {json_resp['success']}"
    assert "message" in json_resp, "Response JSON missing 'message' field"
    assert json_resp["message"] == "Facility not found", f"Expected message 'Facility not found' but got '{json_resp['message']}'"

test_post_api_batch_create_returns_404_for_non_existent_facility()