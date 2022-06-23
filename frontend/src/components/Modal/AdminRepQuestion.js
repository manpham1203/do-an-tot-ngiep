import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux";
import { setCloseadminRepQuestion } from "../../redux/adminRepQuestion/adminRepQuestionActions";
import api from "../../apis/api";
import { FaTimes } from "react-icons/fa";
import { toast } from "react-toastify";

function AdminRepQuestion(props) {
    const dispatch = useDispatch();
    const { adminRepQuestion } = useSelector((s) => s);
    const [text, setText] = useState("");
    const submit = () => {
        const obj = { email: adminRepQuestion.data.email, content: text, question: adminRepQuestion.data.content }
        api({
            method: "GET",
            url: `question/rep`,
            params: obj,
        })
            .then((res) => {
                console.log(res);
                if (res.status === 200) {
                    toast.success(`Gửi liên hệ thành công`, {
                        position: toast.POSITION.TOP_RIGHT,
                        autoClose: 1000,
                    });
                    setText("")
                    dispatch(setCloseadminRepQuestion())
                } else {
                    toast.error(`Thao tác thất bại`, {
                        position: toast.POSITION.TOP_RIGHT,
                        autoClose: 1000,
                    });
                }
            })
            .catch(() =>
                toast.error(`Thao tác thất bại`, {
                    position: toast.POSITION.TOP_RIGHT,
                    autoClose: 1000,
                })
            );
    }
    console.log(adminRepQuestion.data.email, text)
    return (
        <div className="flex flex-col p-[30px] bg-third fixed w-[80%] h-[500px] rounded-xl top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] z-[2000]">
            <div className="w-fit absolute right-0 top-0">
                <FaTimes
                    onClick={() => dispatch(setCloseadminRepQuestion())}
                    className="inline-block text-[30px] text-second cursor-pointer"
                />
            </div>
            <div className='w-full'>
                <h2 className=''>Họ và tên: {adminRepQuestion.data.name}</h2>
                <h2 className=''>Email: {adminRepQuestion.data.email}</h2>
                <h2 className=''>Câu hỏi: {adminRepQuestion.data.content}</h2>
            </div>
            <div className="w-full relative mt-[25px]">
                <textarea
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                    placeholder=" "
                    className="h-[100px] py-[20px] form-input border border-second dark:border-third text-second dark:text-third font-normal  rounded-[4px] w-[100%] px-[20px] transition-all duration-[0.25s] focus:border-second dark:focus:border-third outline-none bg-third dark:bg-darkMode"
                />
                <label className="form-label absolute left-[20px] top-[20%] translate-y-[-50%] pointer-events-none select-none transition-all duration-[0.25s] text-second dark:text-third bg-third dark:bg-darkMode">
                    Viết câu trả lời
                </label>
            </div>
            <button
                type="submit"
                onClick={submit}
                className="bg-second w-[200px] text-third dark:bg-third dark:text-second px-[20px] h-[40px] mt-[25px]"
            >
                GỬI
            </button>
        </div>
    );
}

export default AdminRepQuestion;