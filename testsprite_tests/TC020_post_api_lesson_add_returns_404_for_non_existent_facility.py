import requests

def test_post_api_lesson_add_returns_404_for_non_existent_facility():
    base_url = "http://localhost:5243"
    facility_id = 999999
    url = f"{base_url}/Api/Lesson/Add/{facility_id}"
    # form-data payload as required by test case
    data = {
        'Name': 'Test Lesson',
        'SectionId': '1',
        'Order': '1'
    }
    headers = {
        # No auth required, so no Authorization header
    }
    try:
        response = requests.post(url, data=data, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 404, f"Expected status code 404 but got {response.status_code}"
    try:
        json_resp = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    assert isinstance(json_resp, dict), "Response JSON is not an object"
    # Verify response keys and values
    assert json_resp.get('success') is False, "Expected success to be False"
    message = json_resp.get('message', '').lower()
    assert 'facility not found' in message, f"Expected message to contain 'Facility not found', got: {json_resp.get('message')}"

test_post_api_lesson_add_returns_404_for_non_existent_facility()