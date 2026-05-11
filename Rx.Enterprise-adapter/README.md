# Rx.Enterprise-adapter

Adapter to retrieve information about people and businesses in KISS from Rx.Enterprise.

## Authentication

The Rx.Enterprise API uses mutual TLS (mTLS) — every request must include a client certificate issued per environment.

### Getting the certificates & running locally

The `.crt` and `.key` files are stored in **1Password**. Download both files and place them under:

```
src/RxEnterpriseWorker/certs/client.crt
src/RxEnterpriseWorker/certs/client.key
```

These paths are gitignored.

### Calling the API with curl

```bash
curl --cert certs/client.crt \
     --key  certs/client.key \
     -i 'https://rheden.connect.rx-enterprise-acc.nl/api/zaak/2025-000001'
```

### Running the worker locally

Copy the example env file and fill in the values:

```bash
cp src/RxEnterpriseWorker/.env.example src/RxEnterpriseWorker/.env
```

Then run:

```bash
dotnet run --project src/RxEnterpriseWorker
```
