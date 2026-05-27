import SecureLS from "secure-ls";

export class ApiService {
    static ls = new SecureLS();

    static async request(method, url, data) {
        try {
            const response = await fetch(`http://localhost:5299/api${url}`, {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.ls.get("token")}`
                },
                body: data ? JSON.stringify(data) : undefined,
            });

            return await response.json();
        }
        catch (error) {
            console.error(error);
            return { IsSuccess: false, Error: error.message, Code: 500 };
        }
    }

    static get user() {
        return this.ls.get("user");
    }
}
