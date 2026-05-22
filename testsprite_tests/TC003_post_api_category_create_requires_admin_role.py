import requests

def test_post_api_category_create_requires_admin_role():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Category/Create"
    headers = {
        "Content-Type": "application/json"
    }
    payload = {
        "Name": "Test Category",
        "Description": "This is a test category."
    }
    try:
        response = requests.post(url, json=payload, headers=headers, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed with exception: {e}"

    assert response.status_code == 401, f"Expected status code 401 Unauthorized but got {response.status_code}"