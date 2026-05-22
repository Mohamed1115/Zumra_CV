import requests

def test_get_api_category_returns_all_categories():
    base_url = "http://localhost:5243"
    url = f"{base_url}/Api/Category/GetAll"
    headers = {
        "Accept": "application/json"
    }
    try:
        response = requests.get(url, headers=headers, timeout=30)
        assert response.status_code == 200, f"Expected status code 200, got {response.status_code}"
        json_data = response.json()
        # According to PRD, response is directly a list of categories
        assert isinstance(json_data, list), "Response is not a list"
    except requests.exceptions.RequestException as e:
        assert False, f"Request failed: {e}"

test_get_api_category_returns_all_categories()