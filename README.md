# 🎮 DarkCity - Action RPG

## 📌 프로젝트 개요

| 항목 | 내용 |
| --- | --- |
| 📅 **작업 기간** | 2024.05.09 ~ 2024.06.03 (약 1개월) |
| 🎮 **장르** | 3인칭 액션 RPG |
| 💻 **개발 언어** | C# |
| 🛠️ **개발 엔진** | Unity Engine |
| 👤 **참여 형태** | 개인 프로젝트 |
| 🧩 **담당 역할** | 무기 시스템, AI, 전투 연출 |

---

## 🧠 게임 소개

> C# 기반 Unity Engine 으로 개발한 3인칭 액션 RPG 프로젝트
이 프로젝트는 근접/원거리 무기, 회피, 순간이동 스킬 등 다양한 전투 요소를 데이터 기반 구조로 설계한 개인 개발 프로젝트입니다.

---

## 🧩 주요 기능

### 🗡 무기 시스템 (Fist / 로 무기별 장착/공격 로직 분리 및 통합
- Animation Event를 활용해 히트박스, 발사체, 이펙트 등 타이밍 제어

### 🎯 콤보 시스템
- 콤보 윈도우를 활용해 연속 공격과 분기 콤보 지원
- 무기 데이터에 따라 콤보 개수, 속도, 넉백 효과를 조정

### 🤖 적 AI
- State Machine 기반으로 정찰, 추격, 공격, 회피, 피격 상태 구현
- 거리·시야·체력 조건에 따른 상태 전환 로직 설계

### 💥 전투 연출
- 피격 시 순간 정지(Hit Stop), 넉백, 카메라 쉐이크로 타격감 강화
- 트레일 이펙트 , HP 연동 등으로 투 피드백 제공

---

## 🖼️ 게임 결과 화면

<table>
  <tr>
    <th>플레이어 근거리 공격</th>
    <th>플레이어 순간이동</th>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/4064d488-73f3-49d5-b0d7-84b51b0c2be8" height="200px"></td>
    <td><img src="https://github.com/user-attachments/assets/46eeff12-30ce-4243-9c22-6dc18f4f4bb7" width="350px" height="200px"></td>
  </tr>
  <tr>
    <th>적 AI 추적</th>
    <th>적 AI 원거리 공격</th>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/dc3b2590-263f-4a5a-9d08-9027187baf0b" width="350px" height="200px"></td>
    <td><img src="https://github.com/user-attachments/assets/45d425c6-ec27-48fb-9a64-558f67408bca" width="350px" height="200px"></td>
  </tr>
</table>

---

## 🔗 외부 링크

- 📹 [플레이 영상 보기](https://youtu.be/1by5lFuz6O8)  
- 📄 [개발 명세서 (Notion)](https://melted-part-f0c.notion.site/DarkCity-255924ed314980b89dbcf425b8c9f2b3?source=copy_link)  

---

## 📧 이메일

yea2979@naver.com
