import React, { useEffect, useState } from "react";
import { setOpenAdminViewOrder } from "../../../redux/adminViewOrder/adminViewOrderActions";
import api from "../../../apis/api";
import { toast } from "react-toastify";
import { FaRegEdit, FaRegEye } from "react-icons/fa";
import Tr from "../../Table/Tr";
import Td from "../../Table/Td";

function Row(props) {
    return (
        <Tr>
            <Td>
                
            </Td>
        </Tr>
    );
}

export default Row;