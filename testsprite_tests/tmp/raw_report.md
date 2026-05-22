
# TestSprite AI Testing Report(MCP)

---

## 1️⃣ Document Metadata
- **Project Name:** Zumra
- **Date:** 2026-03-01
- **Prepared by:** TestSprite AI Team

---

## 2️⃣ Requirement Validation Summary

#### Test TC001 get api category returns all categories
- **Test Code:** [TC001_get_api_category_returns_all_categories.py](./TC001_get_api_category_returns_all_categories.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 18, in <module>
  File "<string>", line 11, in test_get_api_category_returns_all_categories
AssertionError: Expected status code 200, got 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/c6f37bda-c2bc-49c1-883c-edfb0ee01439
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC002 get api category by id returns single category
- **Test Code:** [TC002_get_api_category_by_id_returns_single_category.py](./TC002_get_api_category_by_id_returns_single_category.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/212d5cba-20e6-40b6-999f-bd97afd63bdb
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC003 post api category create requires admin role
- **Test Code:** [TC003_post_api_category_create_requires_admin_role.py](./TC003_post_api_category_create_requires_admin_role.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/d45ef917-5aed-4924-874e-ba4ea5ce17df
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC004 put api category update requires admin role
- **Test Code:** [TC004_put_api_category_update_requires_admin_role.py](./TC004_put_api_category_update_requires_admin_role.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/eda23733-7704-4f2c-b688-affbb93e1765
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC005 delete api category requires admin role
- **Test Code:** [TC005_delete_api_category_requires_admin_role.py](./TC005_delete_api_category_requires_admin_role.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/7bafbdc3-b1a4-47d1-8821-1777de052993
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC006 get api course returns all courses
- **Test Code:** [TC006_get_api_course_returns_all_courses.py](./TC006_get_api_course_returns_all_courses.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 32, in <module>
  File "<string>", line 11, in test_get_api_course_returns_all_courses
AssertionError: Expected status code 200, got 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/6bf6c4d2-f2f4-4190-990d-e61f3910b60d
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC007 get api course by id returns 404 for non-existent course
- **Test Code:** [TC007_get_api_course_by_id_returns_404_for_non_existent_course.py](./TC007_get_api_course_by_id_returns_404_for_non_existent_course.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/e03a004f-8025-42b8-91a9-7a68a2d5993b
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC008 post api course create requires authentication
- **Test Code:** [TC008_post_api_course_create_requires_authentication.py](./TC008_post_api_course_create_requires_authentication.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/b897ea9e-09a6-4a3f-90fd-a6ea49b4c4af
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC009 put api course update requires authentication
- **Test Code:** [TC009_put_api_course_update_requires_authentication.py](./TC009_put_api_course_update_requires_authentication.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/1673e9f5-2000-44a0-add9-dcba8dd9d75d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC010 delete api course delete requires authentication
- **Test Code:** [TC010_delete_api_course_delete_requires_authentication.py](./TC010_delete_api_course_delete_requires_authentication.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/9a355c8e-ea9a-4a8e-a68c-33e2e6a9fcca
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC011 get api batch course returns all batches for course
- **Test Code:** [TC011_get_api_batch_course_returns_all_batches_for_course.py](./TC011_get_api_batch_course_returns_all_batches_for_course.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 25, in <module>
  File "<string>", line 18, in test_api_batch_getall_requires_auth_and_returns_list
AssertionError: Expected 200 but got 405

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/57a35067-e100-41ad-9073-4400546633a2
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC012 get api batch by id returns batch
- **Test Code:** [TC012_get_api_batch_by_id_returns_batch.py](./TC012_get_api_batch_by_id_returns_batch.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 51, in <module>
  File "<string>", line 35, in test_get_api_batch_get_all_returns_batches
AssertionError: Expected status code 200 but got 405

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/4a4aa379-ce74-480a-a7c3-aa2949d7fb08
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC013 post api batch create returns 404 for non-existent facility
- **Test Code:** [TC013_post_api_batch_create_returns_404_for_non_existent_facility.py](./TC013_post_api_batch_create_returns_404_for_non_existent_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/3605a198-bd6d-4a69-830e-a9d1faf42b73
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC014 get api group returns all groups for facility
- **Test Code:** [TC014_get_api_group_returns_all_groups_for_facility.py](./TC014_get_api_group_returns_all_groups_for_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/0be1b6f9-82d8-4c58-93de-bae3bdebea10
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC015 get api group by id returns group
- **Test Code:** [TC015_get_api_group_by_id_returns_group.py](./TC015_get_api_group_by_id_returns_group.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 23, in <module>
  File "<string>", line 15, in test_get_api_group_get_all_returns_groups
AssertionError: Expected status code 200 but got 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/a2688f67-002f-4711-91b5-557b1096a17e
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC016 post api group create requires authentication
- **Test Code:** [TC016_post_api_group_create_requires_authentication.py](./TC016_post_api_group_create_requires_authentication.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/2fe78f95-e15f-4ce3-b4d8-6ed617d1613c
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC017 delete api group delete requires authentication
- **Test Code:** [TC017_delete_api_group_delete_requires_authentication.py](./TC017_delete_api_group_delete_requires_authentication.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/743f7746-4fbf-4f2b-a5a8-09fdc9c73905
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC018 post api section create returns 404 for non-existent facility
- **Test Code:** [TC018_post_api_section_create_returns_404_for_non_existent_facility.py](./TC018_post_api_section_create_returns_404_for_non_existent_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/14171ef5-1817-4c93-ab04-e2434ebf0031
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC019 delete api section delete returns 404 for non-existent facility
- **Test Code:** [TC019_delete_api_section_delete_returns_404_for_non_existent_facility.py](./TC019_delete_api_section_delete_returns_404_for_non_existent_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/b76b6f1a-d11e-4dc7-93f3-23448e66699e
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC020 post api lesson add returns 404 for non-existent facility
- **Test Code:** [TC020_post_api_lesson_add_returns_404_for_non_existent_facility.py](./TC020_post_api_lesson_add_returns_404_for_non_existent_facility.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 33, in <module>
  File "<string>", line 21, in test_post_api_lesson_add_returns_404_for_non_existent_facility
AssertionError: Expected status code 404 but got 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/261e826a-4a2f-496c-ab2c-9d385664fa3d
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC021 delete api lesson delete returns 404 for non-existent facility
- **Test Code:** [TC021_delete_api_lesson_delete_returns_404_for_non_existent_facility.py](./TC021_delete_api_lesson_delete_returns_404_for_non_existent_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/0c25853a-f79d-4b79-9adc-957664fdd02b
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC022 post api task add returns 404 for non-existent facility
- **Test Code:** [TC022_post_api_task_add_returns_404_for_non_existent_facility.py](./TC022_post_api_task_add_returns_404_for_non_existent_facility.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 66, in <module>
  File "<string>", line 49, in test_post_api_task_create_with_invalid_course_and_lesson_ids
AssertionError: Expected status 400, got 404

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/69f9e0fe-f55e-440d-8cdc-8658ab4d71e1
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC023 delete api task delete returns 404 for non-existent facility
- **Test Code:** [TC023_delete_api_task_delete_returns_404_for_non_existent_facility.py](./TC023_delete_api_task_delete_returns_404_for_non_existent_facility.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/8b2aaaea-048f-497b-a9ec-76af2563d58d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC024 post api task submit returns 200
- **Test Code:** [TC024_post_api_task_submit_returns_200.py](./TC024_post_api_task_submit_returns_200.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/e6eb6187-2cd3-4db9-8e5d-1962de61090a/f076bb63-d56f-41c2-8143-463339dae650
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---


## 3️⃣ Coverage & Matching Metrics

- **70.83** of tests passed

| Requirement        | Total Tests | ✅ Passed | ❌ Failed  |
|--------------------|-------------|-----------|------------|
| ...                | ...         | ...       | ...        |
---


## 4️⃣ Key Gaps / Risks
{AI_GNERATED_KET_GAPS_AND_RISKS}
---