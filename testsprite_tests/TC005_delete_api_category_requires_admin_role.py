import requests

def test_delete_api_category_requires_admin_role():
    base_url = "http://localhost:5243"
    category_id = 1
    url = f"{base_url}/Api/Category/{category_id}"

    headers = {
        # No Authorization header to simulate unauthorized request
    }

    try:
        response = requests.delete(url, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

    assert response.status_code == 401, f"Expected 401 Unauthorized but got {response.status_code}"
    # Optionally check for error message or response content if provided
    # Many APIs return JSON error message for 401
    try:
        json_resp = response.json()
        # Could assert typical keys like 'message' or 'error', but not mandatory
    except ValueError:
        # Response not JSON is allowed, just pass
        pass

test_delete_api_category_requires_admin_role()