import requests

def test_put_api_course_update_requires_authentication():
    base_url = "http://localhost:5243"
    facility_id = 1
    course_id = 1
    url = f"{base_url}/Api/Course/{facility_id}/{course_id}"
    headers = {
        # No Authorization header to test unauthenticated access
        'Content-Type': 'application/json'
    }
    # Example payload for update; actual schema not detailed but reasonable sample
    payload = {
        "Title": "Updated Course Title",
        "Description": "Updated course description",
        "CategoryId": 1,
        "FacilityId": facility_id,
        "Price": 100
    }

    try:
        response = requests.put(url, headers=headers, json=payload, timeout=30)
    except requests.RequestException as e:
        assert False, f"Request failed: {e}"
    
    assert response.status_code == 401, f"Expected 401 Unauthorized, got {response.status_code}"

test_put_api_course_update_requires_authentication()