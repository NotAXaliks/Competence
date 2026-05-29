```jsx
<ResponsiveContainer height="300">
    <PieChart>
        <Pie data={pieData}>
            <Cell fill={"#1890ff"} />
            <Cell fill={"#52c41a"} />
            <Cell fill={"#faad14"} />
        </Pie>
        <Tooltip />
        <Legend verticalAlign="bottom" />
    </PieChart>
</ResponsiveContainer>
```

```jsx
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
                    <div style={{ display: 'flex', flexDirection: 'column' }}>
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
```