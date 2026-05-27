import { Card, Layout, Statistic, Typography, Row, Col, Progress, Button } from "antd";

export default function Dashboard() {
    return (<Layout>
        <Card title="Рейтинг надежности">
            <div style={{ display: "flex" }}>
                Индекс компетенций:
                <Progress percent={85} strokeColor="#1890ff" style={{width: 500}} />
            </div>

            <div style={{ display: "flex" }}>
                Доверие сообщества:
                <Progress percent={85} strokeColor="#1890ff" style={{width: 500}} />
                (15 подтверждений)
            </div>
        </Card>

        <div style={{ display: "flex" }}>
            <Card style={{ width: 200, height: 120 }}>
                <Statistic title="Подтверждений" value={15} />
            </Card>
            <Card style={{ width: 200, height: 120 }}>
                <Statistic title="Навыков" value={8} />
            </Card>
            <Card style={{ width: 200, height: 120 }}>
                <Statistic title="Рекомендаций" value={3} />
            </Card>
        </div>

        <div style={{ display: "flex" }}>
            <Button>Редактировать профиль</Button>
            <Button>Подтверждения</Button>
            <Button>Экспорт PDF</Button>
        </div>
    </Layout>)
}
