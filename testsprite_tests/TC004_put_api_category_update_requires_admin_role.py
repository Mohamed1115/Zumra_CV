import requests

def test_put_api_category_update_requires_admin_role():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Category/1"
    headers = {
        'Content-Type': 'application/json'
    }
    payload = {
        "Name": "Updated Category Name",
        "Description": "Updated Category Description"
    }
    try:
        response = requests.put(url, json=payload, headers=headers, timeout=30)
        # We expect 401 Unauthorized since no Authorization header is sent
        assert response.status_code == 401, f"Expected 401 Unauthorized, got {response.status_code}"
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

test_put_api_category_update_requires_admin_role()