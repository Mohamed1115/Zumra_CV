import requests

def test_delete_api_group_delete_requires_authentication():
    base_url = "http://localhost:5243"
    url = f"{base_url}/api/Group/1/1"
    headers = {}  # No Authorization header

    try:
        response = requests.delete(url, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 401, f"Expected 401 Unauthorized, got {response.status_code}"

test_delete_api_group_delete_requires_authentication()