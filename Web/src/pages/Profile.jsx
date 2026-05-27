import { Card, Layout, Statistic, Typography, Row, Col, Progress, Tabs, Input, Form, Button, Select, List, Slider, Space, DatePicker } from "antd";
import { DragDropContext, Droppable, Draggable } from '@hello-pangea/dnd';
import { useState, useEffect } from "react";
import { ApiService } from "../services/ApiService";
import dayjs from "dayjs";

function MainData() {
    return (<>
        <Form.Item name="fio" label="ФИО">
            <Input defaultValue={ApiService.user.Name} />
        </Form.Item>
        <Form.Item name="phone" label="Телефон">
            <Input defaultValue={ApiService.user.Phone} />
        </Form.Item>
    </>);
}

function EducationData() {
    const [form] = Form.useForm();

    const expTypes = [{ label: 'Вузы', value: 1 }, { label: 'Курсы', value: 2 }];

    useEffect(() => {
        const fetchData = async () => {
            const res = await ApiService.request("GET", `/users/me`);

            form.setFieldsValue({
                items: res.Data.Educations.map(s => ({
                    type: s.EducationTypeId,
                    name: s.Institution.Name,
                    period: [dayjs(s.StartDate), s.EndDate ? dayjs(s.EndDate) : dayjs()], 
                    document: "Документ"
                }))
            });
        };

        fetchData();
    }, [form]);

    return (
        <Card title="Образование">
            <Form form={form}>
                <Form.List name="items">
                    {(fields, { add, remove }) => (
                        <div style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
                            {fields.map(({ key, name, ...field }) => (
                                <Space key={key}>
                                    <Form.Item {...field} name={[name, 'type']} rules={[{ required: true }]} style={{ width: 120 }}>
                                        <Select placeholder="Тип" options={expTypes} />
                                    </Form.Item>
                                    <Form.Item {...field} name={[name, 'name']}><Input placeholder="Название" /></Form.Item>
                                    <Form.Item {...field} name={[name, 'period']} rules={[{ required: true }]}><DatePicker.RangePicker style={{ width: '100%' }} /></Form.Item>
                                    <Form.Item {...field} name={[name, 'document']}><Input.TextArea placeholder="Документ" /></Form.Item>

                                    <Button danger onClick={() => remove(name)} />
                                </Space>
                            ))}

                            <Button type="dashed" onClick={() => add()} block>Добавить опыт</Button>
                        </div>
                    )}
                </Form.List>
            </Form>
        </Card>
    );
}

function ExperienceData() {
    const [form] = Form.useForm();

    const expTypes = [{ label: 'ТК РФ', value: 'ТК РФ' }, { label: 'Фриланс', value: 'Фриланс' }, { label: 'ИП', value: 'ИП' }];

    useEffect(() => {
        const fetchData = async () => {
            const res = await ApiService.request("GET", `/users/me`);

            form.setFieldsValue({
                items: res.Data.Experiences.map(s => ({
                type: s.EmploymentType,
                company: s.CompanyId.toString(),
                period: [dayjs(s.StartDate), s.EndDate ? dayjs(s.EndDate) : dayjs()], 
                desc: "Описание" }))
            });
        };

        fetchData();
    }, [form]);

    return (
        <Card title="Опыт работы">
            <Form form={form}>
                <Form.List name="items">
                    {(fields, { add, remove }) => (
                        <div style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
                            {fields.map(({ key, name, ...field }) => (
                                <Space key={key}>
                                    <Form.Item {...field} name={[name, 'type']} rules={[{ required: true }]} style={{ width: 120 }}>
                                        <Select placeholder="Тип" options={expTypes} />
                                    </Form.Item>
                                    <Form.Item {...field} name={[name, 'period']} rules={[{ required: true }]}><DatePicker.RangePicker style={{ width: '100%' }} /></Form.Item>
                                    <Form.Item {...field} name={[name, 'company']}><Input placeholder="Компания" /></Form.Item>
                                    <Form.Item {...field} name={[name, 'desc']}><Input.TextArea placeholder="Описание задач" /></Form.Item>

                                    <Button danger onClick={() => remove(name)} />
                                </Space>
                            ))}

                            <Button type="dashed" onClick={() => add()} block>Добавить опыт</Button>
                        </div>
                    )}
                </Form.List>
            </Form>
        </Card>
    );
}

function SkillsData() {
    const [skills, setSkills] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const res = await ApiService.request("GET", `/users/me`);

            console.log(res)

            setSkills(res.Data.UserSkills.map(s => ({ name: s.Skill.Name, level: s.Level })));
        };

        fetchData();
    }, []);

    const [options, setOptions] = useState([]);

    const handleSearch = async (val) => {
        const res = await ApiService.request("GET", `/Skills/suggest?q=${val}`);

        setOptions(res.Data.map(s => ({ value: s, label: s })));
    };

    return (
        <Card title="Навыки">
            <Select showSearch style={{ width: 300 }} onSearch={handleSearch} options={options}
                onSelect={(val) => {
                    if (!skills.find(s => s.name === val)) setSkills([...skills, { name: val, level: 5 }]);
                }} />
            <List dataSource={skills} renderItem={(item, index) => (
                <List.Item
                    actions={[<Button danger onClick={() => setSkills(skills.filter(s => s.name !== item.name))} />]}>

                    <div style={{ flex: 1, marginRight: 20 }}>
                        {item.name} (Уровень: {item.level}/10)
                        <Slider
                            min={0} max={10} value={item.level}
                            onChange={(v) => {
                                const newSkills = [...skills];
                                newSkills[index].level = v;
                                setSkills(newSkills);
                            }}
                        />
                    </div>
                </List.Item>
            )} />
        </Card>
    );
}

const tabs = [
    { key: "main", label: "Общие данные", children: <MainData /> },
    { key: "education", label: "Образование", children: <EducationData /> },
    { key: "experience", label: "Опыт работы", children: <ExperienceData /> },
    { key: "skills", label: "Навыки", children: <SkillsData /> },
];

export default function Profile() {
    return (<Layout>
        <Tabs defaultActiveKey="1" items={tabs} />
    </Layout>)
}
