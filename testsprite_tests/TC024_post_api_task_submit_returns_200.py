import requests

def test_post_api_task_submit_returns_200():
    base_url = "http://localhost:5243"
    task_id = 1
    url = f"{base_url}/Api/Task/Submit/{task_id}"
    headers = {
        "Content-Type": "application/json"
    }
    try:
        response = requests.post(url, headers=headers, timeout=30)
        # The current implementation returns Ok() without logic, so expect HTTP 200 with empty or minimal body
        assert response.status_code == 200, f"Expected status code 200, got {response.status_code}"
    except requests.RequestException as e:
        assert False, f"Request to submit task failed: {e}"

test_post_api_task_submit_returns_200()