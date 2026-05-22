import requests

def test_get_api_group_get_all_returns_groups():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Group/GetAll"
    # Placeholder token for authorization, replace with valid JWT for real test
    token = "your_valid_jwt_token_here"
    headers = {
        "Accept": "application/json",
        "Authorization": f"Bearer {token}"
    }
    timeout = 30
    try:
        response = requests.get(url, headers=headers, timeout=timeout)
        assert response.status_code == 200, f"Expected status code 200 but got {response.status_code}"
        json_data = response.json()
        # According to PRD, response is Group[] list
        assert isinstance(json_data, list), "Response is not a list"
        assert len(json_data) > 0, "Response list is empty"
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

test_get_api_group_get_all_returns_groups()