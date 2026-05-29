import { Layout, Form, message, Input, Checkbox, Button, Card, Space, Typography, Dropdown, Select } from "antd";
import { ApiService } from "../services/ApiService";
import SecureLS from "secure-ls";
import { useState } from "react";

export default function LoginPage() {
    const [page, setPage] = useState("register");

    const onFinishLogin = async (values) => {
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

    const onFinishRegister = async (values) => {
        console.log(values);
    };

    return (<Layout style={{ display: "flex", alignItems: "center" }}>
        <Layout.Content>
            <Typography.Title style={{ textAlign: "center" }}>Личный кабинет</Typography.Title>
            <Select onChange={(value) => setPage(value)} defaultValue={page} options={[{ value: "register", label: "Регистрация" }, { value: "login", label: "Логин" }]} />

            <Card style={{ width: 500 }}>
                {page === "login" ?
                    (<>
                        <Form onFinish={onFinishLogin}>
                            <Form.Item name="Email" label="Email" rules={[{ required: true, type: 'email' }]}>
                                <Input />
                            </Form.Item>

                            <Form.Item name="Password" label="Пароль"
                                rules={[{ required: true, min: 8, message: 'Пароль >8 символов' }]}>
                                <Input.Password />
                            </Form.Item>

                            <Button type="primary" htmlType="submit" block>ВОЙТИ</Button>
                        </Form>

                        <div style={{ display: "flex" }}>
                            <Button>Google</Button>
                            <Button>Github</Button>
                            <Button type="link">Забыли пароль?</Button>
                        </div>
                    </>) : (<>
                        <Form onFinish={onFinishRegister}>
                            <Form.Item name="Email" label="Email" rules={[{ required: true, type: 'email' }]}>
                                <Input />
                            </Form.Item>

                            <Form.Item name="LastName" label="Фамилия" rules={[{ required: true }]}>
                                <Input />
                            </Form.Item>

                            <Form.Item name="FirstName" label="Имя"  rules={[{ required: true }]}>
                                <Input />
                            </Form.Item>

                            <Form.Item name="MiddleName" label="Отчество" rules={[{ required: true }]}>
                                <Input />
                            </Form.Item>
                        
                            <Form.Item name="Password" label="Пароль" rules={[{ required: true }]}>
                                <Input.Password />
                            </Form.Item>

                            <Button htmlType="submit">Зарегистрироваться</Button>
                        </Form>
                    </>)
                }
            </Card>
        </Layout.Content>
    </Layout>)
}
