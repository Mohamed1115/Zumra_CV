import requests

def test_delete_api_course_delete_requires_authentication():
    base_url = "http://localhost:5243"
    path = "/Api/Course/1/1"
    url = base_url + path

    try:
        response = requests.delete(url, timeout=30)
        # Should return 401 Unauthorized due to missing Authorization header
        assert response.status_code == 401, f"Expected 401 Unauthorized, got {response.status_code}"
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"

test_delete_api_course_delete_requires_authentication()