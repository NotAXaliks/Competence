import { Layout, Form, message, Input, Checkbox, Button, Card, Space, Typography } from "antd";
import { ApiService } from "../services/ApiService";
import SecureLS from "secure-ls";
import { Content, Footer } from "antd/es/layout/layout";

export default function LoginPage() {
    const onFinish = async (values) => {
        try {
            const resp = await ApiService.request("POST", "/auth/login", values);

            if (resp.Data.Token) {
                ApiService.ls.set("token", resp.Data.Token);
                ApiService.ls.set("user", resp.Data.User)

                message.success('Успешный вход');
                window.location.href = '/dashboard';
            }
        } catch (err) {
            const msg = err.response?.status === 401 ? 'Неверный email или пароль' : 'Сервер недоступен';
            message.error(msg);
        }
    };

    return (<Layout style={{ display: "flex", alignItems: "center" }}>
        <Content>
            <Card style={{ width: 500 }}>
                <div style={{ textAlign: "center", marginBottom: 20 }}>
                    <Typography.Title>ИТ-Проф</Typography.Title>

                    <Space size={16}>
                        <Typography.Text>Личный кабинет HR</Typography.Text>
                    </Space>
                </div>

                <Form onFinish={onFinish} layout="vertical">
                    <Form.Item name="Email" label="Email" rules={[{ required: true, type: 'email' }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="Password" label="Пароль"
                        rules={[{ required: true, min: 3, message: 'Пароль > 3 символов' }]}>
                        <Input.Password />
                    </Form.Item>

                    <Button type="primary" htmlType="submit" block>ВОЙТИ</Button>
                </Form>

                <div style={{ display: "flex" }}>
                    <Button>Google</Button>
                    <Button>Github</Button>
                    <Button type="link">Забыли пароль?</Button>
                </div>
            </Card>
        </Content>

        <Footer>Версия 1.0.0</Footer>
    </Layout>)
}
