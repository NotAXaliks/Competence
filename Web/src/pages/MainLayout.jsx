import { Layout as AntLayout, Menu, Typography, Image, Button } from 'antd';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import { ApiService } from '../services/ApiService';

const { Header, Content, Sider } = AntLayout;

export const MainLayout = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const menuItems = [
    { key: '/dashboard', label: 'Главная' },
    { key: '/profile', label: 'Редактировать профиль' },
    { key: '/confirmations', label: 'Подтверждения' },
    { key: '/rating', label: 'Рейтинг и рекомендации' },
  ];

  const onLogout = () => {
    ApiService.ls.remove("token");
    ApiService.ls.remove("user");

    window.location.href = '/';
  };

  return (
    <AntLayout style={{ minHeight: '100vh' }}>
      <Header>
        <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
          <Typography.Title level={1} style={{ color: "white" }}>ИТ Проф</Typography.Title>
          <Image src={`data:image/jpeg;base64,${ApiService.ls.get("user").Avatar}`} style={{ width: 40, height: 40 }} />
          <Typography.Title level={4} style={{ color: "white" }}>{ApiService.ls.get("user").Name}</Typography.Title>
          <Button onClick={onLogout}>Выход</Button>
        </div>
      </Header>

      <Content>
        <AntLayout style={{ minHeight: '100vh'}}>
          <Sider>
            <Menu
              theme="dark"
              mode="inline"
              selectedKeys={[location.pathname]}
              onClick={({ key }) => navigate(key)}
              items={menuItems}
            />
          </Sider>

          <Content>
            <Outlet />
          </Content>
        </AntLayout>
      </Content>
    </AntLayout>
  );
};
