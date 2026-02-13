# JWT Implementation Feedback

## What is good
- JWT generation and validation are encapsulated behind `IJWTHelper`, which keeps controller code simpler.
- Token signing uses `HmacSha256` with a symmetric key.
- Expiration is set and lifetime validation is enabled.
- `ClockSkew` is explicitly set to zero, which makes expiry behavior deterministic.

## Issues that likely hurt interview evaluation

### 1) Audience validation is disabled
- In `GetTokenValidationParameters()`, `ValidateAudience` is set to `false` even though `ValidAudience` is configured.
- This weakens token validation and is often treated as a security gap in JWT reviews.

### 2) Shared/static password salt
- Password hashing uses one global salt from config (`PasswordSalt`) for every user.
- A per-user random salt should be used and stored with each user. Reusing a global salt makes offline attacks easier if the database is leaked.

### 3) Weak PBKDF2 settings by current standards
- PBKDF2 uses HMACSHA1 and 10,000 iterations.
- Interviewers commonly expect stronger defaults now (e.g., PBKDF2-HMACSHA256 with much higher iterations, or ASP.NET Core `PasswordHasher<TUser>`).

### 4) Sensitive secrets are committed in appsettings
- JWT signing key and password salt appear directly in `appsettings.json`.
- Interview reviewers usually expect secret manager / environment variables / vault usage.

### 5) Minimal JWT claims
- Token currently includes only email claim.
- Missing `sub`/user-id, `jti`, and authorization-related claims (if applicable), making revocation/auditing harder.

### 6) Login/register endpoint shape and response semantics
- `Login` and `Register` accept raw method parameters without request DTOs and validation annotations.
- Login failure returns string `"Not Implimented"` instead of 401/400 with a structured payload.
- These API contract issues can count against production readiness.

### 7) Exception handling anti-pattern
- `throw ex;` is used in `ConfigReader`, which resets stack trace.
- Reviewers generally expect `throw;` if rethrowing.

## Suggested remediation order
1. Enable and correctly validate audience/issuer/signing key.
2. Move secrets out of source control.
3. Replace password hashing with ASP.NET Core `PasswordHasher<TUser>` (or stronger PBKDF2/Argon2 strategy with per-user salt).
4. Improve auth API contract (DTOs + model validation + proper HTTP status codes).
5. Add richer token claims (`sub`, `jti`) and optional refresh-token flow.

## Quick win checklist
- [ ] `ValidateAudience = true`
- [ ] Per-user random salt + stronger hashing setup
- [ ] Remove secrets from `appsettings.json`
- [ ] Return `Unauthorized()` on invalid login
- [ ] Use request models with `[Required]`, email format, min password length
