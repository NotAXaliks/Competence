import { Card, Layout, Statistic, Typography, Row, Col, Progress, Button, Divider } from "antd";

export default function Dashboard() {
    return (<Layout>
        <Card title="Рейтинг надежности">
            Индекс компетенций:
            <Progress percent={85} strokeColor="#1890ff" style={{width: 500}} />
            <Divider />
            Доверие сообщества:
            <Progress percent={85} strokeColor="#1890ff" style={{width: 500}} />
            (15 подтверждений)
        </Card>

        <Statistic title="Подтверждений" value={15} />
        <Statistic title="Навыков" value={8} />
        <Statistic title="Рекомендаций" value={3} />

        <div style={{ display: "flex" }}>
            <Button>Редактировать профиль</Button>
            <Button>Подтверждения</Button>
            <Button>Экспорт PDF</Button>
        </div>
    </Layout>)
}
