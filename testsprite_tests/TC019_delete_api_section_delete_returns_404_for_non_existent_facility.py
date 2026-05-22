import requests

def test_TC019_delete_api_section_delete_returns_404_for_non_existent_facility():
    base_url = "http://localhost:5243"
    timeout_seconds = 30

    # Endpoint to test
    facility_id = 999999
    batch_id = 1
    section_id = 1
    url = f"{base_url}/Api/Section/{facility_id}/{batch_id}/{section_id}"

    try:
        response = requests.delete(url, timeout=timeout_seconds)
    except requests.RequestException as e:
        assert False, f"Request to {url} failed: {e}"

    assert response.status_code == 404, f"Expected status 404 but got {response.status_code}"

    try:
        json_response = response.json()
    except ValueError:
        assert False, "Response is not valid JSON"

    assert json_response.get("success") is False or json_response.get("success") == "false", \
        f"Expected success=false in response, got: {json_response.get('success')}"
    message = json_response.get("message", "").lower()
    assert "facility not found" in message, f"Expected message containing 'Facility not found', got: {json_response.get('message')}"

test_TC019_delete_api_section_delete_returns_404_for_non_existent_facility()