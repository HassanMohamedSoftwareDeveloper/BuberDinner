# Buber Dinner API
- [Buber Dinner Api](#buber-dinner-api)
	- [Auth](#auth)
		- [Register](#register)
			- [Register Request](#register-request)
			- [Register Response](#register-response)
		- [Login](#login)
			- [Login Request](#login-request)
			- [Login Response](#login-response)
## Auth
### Register

```js
POST {{host}}/auth/register
```
### Register Request

```json
{
	"firstName":"Hassan",
	"lastName":"Mohamed",
	"email":"hassanmohamed_hm@hotmail.com",
	"password":"passw0rd"
}
```
### Register Response

```js
200 OK
```

```json
{
	"id":"d89c2d9a-eb3e-4075-95ff-b920b55aa104",
	"firstName":"Hassan",
	"lastName":"Mohamed",
	"email":"hassanmohamed_hm@hotmail.com",
	"token":"eyJhb..z9dqcnYoY"
}
```

### Login

```js
POST {{host}}/auth/login
```
### Login Request

```json
{
	"email":"hassanmohamed_hm@hotmail.com",
	"password":"passw0rd"
}
```
### Login Response

```js
200 OK
```

```json
{
	"id":"d89c2d9a-eb3e-4075-95ff-b920b55aa104",
	"firstName":"Hassan",
	"lastName":"Mohamed",
	"email":"hassanmohamed_hm@hotmail.com",
	"token":"eyJhb..z9dqcnYoY"
}
```