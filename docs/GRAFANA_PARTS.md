# Grafana Parts in This Project

This document lists **every Grafana-related part** in the solution and where it is documented. Use it to verify that all parts are covered.

**Grafana URL:** http://localhost:3000  
**Credentials:** See [CREDENTIALS.md](CREDENTIALS.md#grafana).

---

## Checklist: All Parts

| Part | Description | Where documented |
|------|-------------|------------------|
| **Grafana application** | Observability UI (Explore, dashboards, data sources). | [APPS.md – Grafana](APPS.md#grafana), [CREDENTIALS.md – Grafana](CREDENTIALS.md#grafana) |
| **Explore** | UI to run queries and view traces/logs. | [GRAFANA_USAGE.md – § 1](GRAFANA_USAGE.md#1-see-the-data-sent-to-grafana) |
| **Tempo data source** | Trace store; view distributed traces (Blazor ↔ API). | [GRAFANA_USAGE.md – View traces (Tempo)](GRAFANA_USAGE.md#view-traces-tempo), [APPS.md – Tempo](APPS.md#tempo) |
| **Loki data source** | Log store; view application logs. | [GRAFANA_USAGE.md – View logs (Loki)](GRAFANA_USAGE.md#view-logs-loki), [APPS.md – Loki](APPS.md#loki) |
| **Trace-to-logs** | Link from a Tempo span to related logs in Loki. | [GRAFANA_USAGE.md – Trace-to-logs](GRAFANA_USAGE.md#trace-to-logs-already-configured) |
| **Provisioned data sources** | Tempo and Loki added via YAML (no UI setup). | [GRAFANA_USAGE.md – § 2 (YAML)](GRAFANA_USAGE.md#add-a-data-source-by-provisioning-yaml), [Provisioning layout](#provisioning-layout) below |
| **Adding a data source (UI)** | How to add another data source in Grafana. | [GRAFANA_USAGE.md – Add data source (UI)](GRAFANA_USAGE.md#add-a-data-source-in-the-ui) |
| **Adding a data source (YAML)** | How to add by provisioning. | [GRAFANA_USAGE.md – Add data source (YAML)](GRAFANA_USAGE.md#add-a-data-source-by-provisioning-yaml) |
| **OpenTelemetry Collector** | Receives OTLP from apps; sends traces → Tempo, logs → Loki. | [APPS.md – OpenTelemetry Collector](APPS.md#opentelemetry-collector) |
| **Tempo (service)** | Backend that stores traces. | [APPS.md – Tempo](APPS.md#tempo) |
| **Loki (service)** | Backend that stores logs. | [APPS.md – Loki](APPS.md#loki) |

---

## Provisioning layout

Grafana is provisioned only with **data sources**. No dashboards or alerting are provisioned in this project.

| Item | Path | Contents |
|------|------|----------|
| **Data sources** | `docker/grafana/provisioning/datasources/datasources.yaml` | **Tempo** (url: `http://tempo:3200`, trace-to-logs → Loki) and **Loki** (url: `http://loki:3100`, default datasource). |

After changing YAML, restart Grafana so it reloads:

```bash
docker compose restart grafana
```

---

## Backend config files (Docker)

These are not inside Grafana but feed the data Grafana shows:

| File | Purpose |
|------|---------|
| `docker/otel-collector-config.yaml` | OTLP receiver; pipelines: traces → Tempo, logs → Loki. |
| `docker/tempo-config.yaml` | Tempo server config (storage, etc.). |
| `docker/loki-config.yaml` | Loki server config (storage, schema). |

Documented in [APPS.md](APPS.md) under each service (OpenTelemetry Collector, Tempo, Loki).

---

## Summary

- **Using Grafana (traces, logs, data sources):** [GRAFANA_USAGE.md](GRAFANA_USAGE.md)  
- **Credentials and apps:** [CREDENTIALS.md](CREDENTIALS.md), [APPS.md](APPS.md)  
- **Documentation index:** [README.md](README.md)

All Grafana parts listed above are documented in the linked locations.
