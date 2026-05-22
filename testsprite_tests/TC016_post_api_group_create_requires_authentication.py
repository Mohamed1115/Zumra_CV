import requests

def test_post_api_group_create_requires_authentication():
    base_url = "http://localhost:5243"
    facility_id = 1
    url = f"{base_url}/api/Group/{facility_id}"
    json_payload = {
        "BatchId": 1,
        "Name": "Test Group Name",
        "Capacity": 10
    }
    headers = {
        "Content-Type": "application/json"
    }
    try:
        response = requests.post(url, json=json_payload, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"
    assert response.status_code == 401, f"Expected 401 Unauthorized but got {response.status_code}"
    
test_post_api_group_create_requires_authentication()