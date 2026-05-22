import requests

BASE_URL = "http://localhost:5243"
TIMEOUT = 30

# Placeholder token for auth, this should be replaced with valid JWT for actual tests
TOKEN = "your_jwt_token_here"


def test_api_batch_getall_requires_auth_and_returns_list():
    headers = {
        "Accept": "application/json",
        "Authorization": f"Bearer {TOKEN}"
    }

    url = f"{BASE_URL}/Api/Batch/GetAll"
    response = requests.get(url, headers=headers, timeout=TIMEOUT)
    assert response.status_code == 200, f"Expected 200 but got {response.status_code}"
    json_data = response.json()

    # The response should be a list of Batch objects
    assert isinstance(json_data, list), f"Expected list but got {type(json_data)}"


test_api_batch_getall_requires_auth_and_returns_list()