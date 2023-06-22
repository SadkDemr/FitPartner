import React, { useEffect, useRef } from "react";
import { Calendar } from "@fullcalendar/core";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from "@fullcalendar/interaction";
import { Modal, Button, Table } from "react-bootstrap";
import moment from 'moment';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
    const calendarRef = useRef(null);
    const [modalShow, setModalShow] = React.useState(false);
    const [eventInfo, setEventInfo] = React.useState({
        title: '',
        description: '',
        start: '',
        end: ''
    });

    useEffect(() => {
        const calendarEl = calendarRef.current;

        const initialDate = localStorage.getItem('currentDate') || new Date().toISOString().slice(0, 10);
        const initialView = localStorage.getItem('currentView') || 'dayGridMonth';

        const calendar = new Calendar(calendarEl, {
            plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            initialDate: initialDate,
            initialView: initialView,
            locale: "tr",
            navLinks: true,
            selectable: true,
            selectMirror: true,
            themeSystem: "bootstrap",
            eventClick: function (arg) {
                setEventInfo({
                    title: arg.event.title,
                    description: arg.event.extendedProps.description,
                    start: arg.event.start.toLocaleString(),
                    end: arg.event.end ? arg.event.end.toLocaleString() : 'Belirtilmemiþ'
                });
                setModalShow(true);
            },
            editable: true,
            dayMaxEvents: true,
            events: "/Student/etkinlikGetir"
        });

        calendar.render();
    }, []);

    return (
        <div className="container-fluid">
            <div ref={calendarRef} />
            <Modal
                show={modalShow}
                onHide={() => setModalShow(false)}
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Görev Detaylarý
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Table responsive hover>
                        <tbody>
                            <tr>
                                <th>Baþlýk</th>
                                <td>{eventInfo.title}</td>
                            </tr>
                            <tr>
                                <th>Açýklama</th>
                                <td>{eventInfo.description}</td>
                            </tr>
                            <tr>
                                <th>Baþlangýç tarihi</th>
                                <td>{moment(eventInfo.start).format('MMMM Do YYYY, h:mm:ss a')}</td>
                            </tr>
                            <tr>
                                <th>Bitiþ tarihi</th>
                                <td>{moment(eventInfo.end).format('MMMM Do YYYY, h:mm:ss a')}</td>
                            </tr>
                        </tbody>
                    </Table>
                </Modal.Body>
            </Modal>
        </div>
    );
}

export default App;